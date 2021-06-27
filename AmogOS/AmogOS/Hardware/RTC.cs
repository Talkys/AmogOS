using System;
using System.Collections.Generic;
using System.Text;
using COSMOS_RTC = Cosmos.HAL.RTC;

namespace AmogOS.Hardware
{
    public static class RTC
    {
        /*Hora*/
        public static int getHour()   { return COSMOS_RTC.Hour; }
        public static int getMinute() { return COSMOS_RTC.Minute; }
        public static int getSecond() { return COSMOS_RTC.Second; }

        /*Hora em string*/
        public static string getTime() { return COSMOS_RTC.Hour.ToString() + ":" + COSMOS_RTC.Minute.ToString() + ":" + COSMOS_RTC.Second.ToString(); }

        /*Data formatada*/
        public static string getDateFormatted()
        {
            string date = "00/00/0000";
            date = COSMOS_RTC.Month + "/" + COSMOS_RTC.DayOfTheMonth + "/20" + COSMOS_RTC.Year;
            return date;
        }

        /*Hora formatada*/
        public static string getTimeFormatted()
        {
            string hour, minute;

            // formatar hora
            int hr; bool morning = true;
            if (getHour() <= 12) { hr = getHour(); if (hr < 11) { morning = true; } }
            else { hr = getHour() - 12; if (hr < 12) { morning = false; } }

            // formatar hora
            if (hr < 10) { hour = "0" + hr.ToString(); }
            if (hr == 0) { hour = "12"; }
            else { hour = hr.ToString(); }

            // formatar minuto
            if (COSMOS_RTC.Minute < 10) { minute = "0" + COSMOS_RTC.Minute; }
            else { minute = COSMOS_RTC.Minute.ToString(); }

            // am ou pm?
            if (morning) { return hour + ":" + minute + " AM"; }
            else { return hour + ":" + minute + " PM"; }
        }
    }
}
