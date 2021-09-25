from datetime import datetime, timezone
import pytz
import pandas as pd
import sched, time
from skyfield.api import load, wgs84

schedule = sched.scheduler(time.time, time.sleep)

def calculate_initial_paramenters(repeating_schedule): 
    # Load debris
    stations_url = 'https://celestrak.com/NORAD/elements/1999-025.txt'
    satellites = load.tle_file(stations_url)

    # Make a list of 100 days
    today_date = datetime.now(timezone.utc)
    today_date = today_date.replace(tzinfo=pytz.utc)
    date_list = pd.date_range(start = today_date, periods = 100, freq='D').to_pydatetime().tolist()

    # Load timescale
    ts = load.timescale()
    t = ts.utc(date_list)

    # Generate a dictionary for satelite data
    sat_data = {}
    longitude = []
    latitude = []
    altitude = []
    name = []
    epoch = []

    # Generate satelite data
    for sat in satellites:
        geocentric = sat.at(t)
        subpoint = wgs84.subpoint(geocentric)
        latitude.append(subpoint.latitude.degrees)
        longitude.append(subpoint.longitude.degrees)
        altitude.append(format(subpoint.elevation.km))
        name.append(sat.name)
        temp = str(sat).split(' ')
        epoch.append(str(temp[6] + " " + temp[7]))

        sat_data["latitude"] = latitude
        sat_data["longitude"] = longitude
        sat_data["name"] = name
        sat_data["epoch"] = epoch
        print(sat_data)
        
        # Repeat after 0.1 seconds
        schedule.enter(0.1, 1, calculate_initial_paramenters, (repeating_schedule,))

# Start after 0.1 seconds
schedule.enter(0.1, 1, calculate_initial_paramenters, (schedule,))
schedule.run()
