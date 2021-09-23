from skyfield.api import load, wgs84
import pandas as pd
from skyfield.api import EarthSatellite
from datetime import datetime as dt
import json
from skyfield.positionlib import Geocentric

# stations_url = 'https://celestrak.com/NORAD/elements/1999-025.txt'
satelite_file = "./test"
satellites = load.tle_file(satelite_file + '.txt')
print('Loaded', len(satellites), 'debris')

ts = load.timescale()
t = ts.now()

i = 0
sat_data = {}
for sat in satellites:
    geocentric = sat.at(t)
    print(sat.name)
    subpoint = wgs84.subpoint(geocentric)
    this_latitude = subpoint.latitude.degrees
    this_longitude = subpoint.longitude.degrees
    print('Latitude:', this_latitude)
    print('Longitude:', this_longitude)
    print('Height: {:.1f} km'.format(subpoint.elevation.km))
    sat_data[i] = {
            "Latitude": str(this_latitude),
            "Longitude": str(this_longitude)
    }
    i+=1

sat_data_json = json.dumps(sat_data, indent = 4)
# print(sat_data_json)
with open(satelite_file + ".json", "w") as outfile:
    json.dump(sat_data_json, outfile)