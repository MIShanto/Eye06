from django.shortcuts import render
from django.http import HttpResponse
from datetime import datetime, timezone
import pytz
import pandas as pd

# from rest_framework import viewsets
# import requests

from skyfield.api import load, wgs84
# from skyfield.api import EarthSatellite
import json

def nan_checker(abc):
        temp = '{:.4f}'.format(abc)
        if  temp == "nan":
            return "0"
        return temp

def satelite_data(stations_url):
    # textfile = requests.get(stations_url)
    satellites = load.tle_file(stations_url)

    ts = load.timescale()
    t = ts.now()

    # i = 0
    sat_data = {}
    longitude = []
    latitude = []
    altitude = []
    name = ""
    # epoch = []
    for sat in satellites:
        geocentric = sat.at(t)
        subpoint = wgs84.subpoint(geocentric)
        latitude.append(nan_checker(subpoint.latitude.degrees))
        longitude.append(nan_checker(subpoint.longitude.degrees))
        name = sat.name
        # temp = str(sat).split(' ')
        # epoch.append(str(temp[6] + " " + temp[7]))
        altitude.append(nan_checker(subpoint.elevation.km))

    sat_data["latitude"] = latitude
    sat_data["longitude"] = longitude
    sat_data["name"] = name
    sat_data["altitude"] = altitude
    print(max([float(x) for x in altitude]))
    print(min([float(x) for x in altitude]))

    # sat_data["epoch"] = epoch
    # print(sat_data["latitude"][0])
    return sat_data

# Create your views here.
url_1 = 'https://celestrak.com/NORAD/elements/1999-025.txt'
url_2 = 'https://celestrak.com/NORAD/elements/2019-006.txt'
url_3 = 'https://celestrak.com/NORAD/elements/iridium-33-debris.txt'
url_4 = 'https://celestrak.com/NORAD/elements/cosmos-2251-debris.txt'



def calculate_initial_paramenters(stations_url): 
    # Load debris
    satellites = load.tle_file(stations_url)

    # Make a list of 100 days
    today_date = datetime.now(timezone.utc)
    today_date = today_date.replace(tzinfo=pytz.utc)
    date_list = pd.date_range(start = today_date, periods = 43200, freq='S').to_pydatetime().tolist()
    # print(date_list)
    # Load timescale
    ts = load.timescale()
    t = ts.utc(date_list)
    #print(t)
    # Generate a dictionary for satelite data
    sat_data = {}
    longitude = []
    latitude = []
    altitude = []
    epoch = []

    # Generate satelite data
    i = 0
    total_data = {}
    for sat in satellites:
        geocentric = sat.at(t)
        # #print(geocentric)
        subpoint = wgs84.subpoint(geocentric)
        individual_data = {
        "latitude" : [nan_checker(a) for a in (subpoint.latitude.degrees)],
        "longitude" : [nan_checker(a) for a in (subpoint.longitude.degrees)],
        "altitude" : [nan_checker(a) for a in (subpoint.elevation.km)]

        }
        total_data[i] = individual_data
        i = i + 1
    return total_data





def debri_1(request):
    data = json.dumps(satelite_data(url_1))
    return HttpResponse(data)


def debri_2(request):
    data = json.dumps(satelite_data(url_2))
    return HttpResponse(data)


def debri_3(request):
    data = json.dumps(satelite_data(url_3))
    return HttpResponse(data)


def debri_4(request):
    data = json.dumps(satelite_data(url_4))
    return HttpResponse(data)

# def pred_1(request):
#     data = json.dumps(calculate_initial_paramenters(url_1))
#     return HttpResponse(data)

def pred_2(request):
    data = json.dumps(calculate_initial_paramenters(url_2))
    return HttpResponse(data)

# def pred_3(request):
#     data = json.dumps(calculate_initial_paramenters(url_3))
#     return HttpResponse(data)

# def pred_4(request):
#     data = json.dumps(calculate_initial_paramenters(url_4))
#     return HttpResponse(data)