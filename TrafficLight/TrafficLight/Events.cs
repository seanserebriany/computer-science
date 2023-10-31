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
        public static Action<trafficlightsstate> OnChangeState;
    }
}
