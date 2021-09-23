from skyfield.api import load, wgs84
import pandas as pd
from skyfield.api import EarthSatellite
from datetime import datetime as dt
import json
from skyfield.positionlib import Geocentric

# def infinite(t):
#     while True:
#     time.sleep(60)


# stations_url = 'https://celestrak.com/NORAD/elements/1999-025.txt'
satellites = load.tle_file("./test.txt")
print('Loaded', len(satellites), 'debris')

# date_list = pd.date_range(datetime.today(), periods=100).tolist()

# this_month = int(date_list[0].strftime("%m"))
# this_year = int(date_list[0].strftime("%Y"))
# this_date = int(date_list[0].strftime("%d"))

ts = load.timescale()
t = ts.now()

for sat in satellites:
    geocentric = sat.at(t)
    print(sat.name)
    subpoint = wgs84.subpoint(geocentric)
    print('Latitude:', subpoint.latitude)
    print('Longitude:', subpoint.longitude)
    print('Height: {:.1f} km'.format(subpoint.elevation.km))
    sat_dict = {
        "name": sat.name,
        "Latitude": str(subpoint.latitude),
        "Longitude": str(subpoint.longitude)
    }
    with open('data.json', 'w') as outfile:
        json.dump(sat_dict, outfile)
