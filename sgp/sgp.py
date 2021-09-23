from skyfield.api import load, wgs84
from skyfield.api import EarthSatellite

stations_url = 'https://celestrak.com/NORAD/elements/1999-025.txt'
satellites = load.tle_file(stations_url)
print('Loaded', len(satellites), 'debris')

ts = load.timescale()
t = ts.now()
for sat in satellites:
    print(sat.at(t).position.km)