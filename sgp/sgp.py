from skyfield.api import load, wgs84
import pandas as pd
from skyfield.api import EarthSatellite
from datetime import datetime

# def infinite(t):
#     while True:
#     time.sleep(60)


# stations_url = 'https://celestrak.com/NORAD/elements/1999-025.txt'
satellites = load.tle_file("./test.txt")
print('Loaded', len(satellites), 'debris')

date_list = pd.date_range(datetime.today(), periods=100).tolist()

this_month = int(date_list[0].strftime("%m"))
this_year = int(date_list[0].strftime("%Y"))
this_date = int(date_list[0].strftime("%d"))

ts = load.timescale()
t = ts.utc(this_year, this_month, this_date)
for sat in satellites:
    print(str(sat) + " " + str(sat.at(t).position.km))