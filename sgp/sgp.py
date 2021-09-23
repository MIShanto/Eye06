from sgp4 import omm
from sgp4.api import Satrec

from skyfield.api import load, wgs84

stations_url = 'https://celestrak.com/NORAD/elements/1999-025.txt'
satellites = load.tle_file(stations_url)
print('Loaded', len(satellites), 'debris')

by_name = {sat.name: sat for sat in satellites}
#satellite = by_name['FENGYUN 1C DEB']
print(by_name)









from skyfield.api import EarthSatellite
import datetime

ts = load.timescale()
t = ts.utc(2018, 7, 31)
#t = ts.now()


# line1 = '1 44383U 19006DE  21265.48736934  .00034058  10198-5  45670-3 0  9997'
# line2 = '2 44383  96.1742 273.4086 0633428 125.2000 241.0218 14.50379804121473'
# satellite = EarthSatellite(line1, line2, 'MICROSAT-R DEB', ts)
# print(satellite.at(t).position.km)
