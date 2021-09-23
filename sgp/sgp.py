from skyfield.api import load, wgs84
import pandas as pd
from skyfield.api import EarthSatellite
from datetime import datetime

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
    subpoint = wgs84.subpoint(geocentric)
    print('Latitude:', subpoint.latitude)
    print('Longitude:', subpoint.longitude)
    print('Height: {:.1f} km'.format(subpoint.elevation.km))