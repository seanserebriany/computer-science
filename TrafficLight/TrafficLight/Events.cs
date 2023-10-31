using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TrafficLight.objects.Trafficlights;

namespace TrafficLight
{
    public static class Events
    {
        private static Action<trafficlightsstate> onChangeState;
        internal static Action<trafficlightsstate> OnChangeState { get => onChangeState; set => onChangeState = value; }
    }
}
