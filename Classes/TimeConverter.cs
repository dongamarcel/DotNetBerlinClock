using System;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        private const string LAMP_YELLOW= "Y";
        private const string LAMP_RED = "R";
        private const string LAMP_OFF = "O";
        private const string LAMP_15_MINUTES = "YYY";
        private const string LAMP_QUARTER_HOUR = "YYR";

        public string convertTime(string aTime)
        {
            var timePieces = Array.ConvertAll(aTime.Split(':'), pieceOfTime => Convert.ToInt16(pieceOfTime));
            var clockBuiler = new StringBuilder();

            return clockBuiler.AppendLine(GetSecondsRow(timePieces[2]))
                .AppendLine(GetTopHoursRow(timePieces[0]))
                .AppendLine(GetBottomHoursRow(timePieces[0]))
                .AppendLine(GetTopMinutesRow(timePieces[1]))
                .Append(GetBottomMinutesRow(timePieces[1])).ToString();
        }

        private string GetSecondsRow(int seccondsPiece)
        {
            if (seccondsPiece % 2 == 0)
            {
                return LAMP_YELLOW;
            }

            return LAMP_OFF;            
        }

        private string GetTopHoursRow(int hoursPiece)
        {
            return GetLampOnOrOff(4, GetNumberOfSignsTurnedOn(hoursPiece));
        }

        private string GetBottomHoursRow(int hoursPiece)
        {
            return GetLampOnOrOff(4, hoursPiece % 5);
        }

        private string GetTopMinutesRow(int minutesPiece)
        {
            return GetLampsOnOrOff(11, GetNumberOfSignsTurnedOn(minutesPiece), LAMP_YELLOW).Replace(LAMP_15_MINUTES, LAMP_QUARTER_HOUR);
        }

        private string GetBottomMinutesRow(int minutesPiece)
        {
            return GetLampsOnOrOff(4, minutesPiece % 5, LAMP_YELLOW);
        }

        private int GetNumberOfSignsTurnedOn(int timePiece)
        {
            return (timePiece - (timePiece % 5)) / 5;
        }

        private string GetLampOnOrOff(int lampsQuantity, int signsTunedOn)
        {
            return GetLampsOnOrOff(lampsQuantity, signsTunedOn, LAMP_RED);
        }

        private string GetLampsOnOrOff(int lampsQuantity, int signsTunedOn, string onSignString)
        {
            var lampOnOffBuilder = new StringBuilder();
            for (int i = 0; i < signsTunedOn; i++)
            {
                lampOnOffBuilder.Append(onSignString);
            }

            for (int i = 0; i < (lampsQuantity - signsTunedOn); i++)
            {
                lampOnOffBuilder.Append(LAMP_OFF);
            }

            return lampOnOffBuilder.ToString();
        }
    }
}
