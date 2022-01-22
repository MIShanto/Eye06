from django.urls import path

from . import views;

urlpatterns = [
    path("1/", views.debri_1),
    path("2/", views.debri_2),
    path("3/", views.debri_3),
    path("4/", views.debri_4),
    # path("p1/", views.pred_1),
    path("p2/", views.pred_2),
    # path("p3/", views.pred_3),
    # path("p4/", views.pred_4),
]