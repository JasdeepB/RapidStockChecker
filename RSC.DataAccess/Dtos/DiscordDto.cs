using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSC.DataAccess.Dtos
{
    public class DiscordDto
    {
        public int Channel { get; set; }
        public int Role { get; set; }
        public int SleepTime { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
    }
}
