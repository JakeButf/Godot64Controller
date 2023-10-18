using System;
using System.Collections.Generic;

namespace n64proofofconcept.scripts.player.platformercontroller
{
	internal class PlatformerTimer
	{
		internal static List<PlatformerTimer> Timers = new List<PlatformerTimer>();
		

		public bool hasEndTime;

		public float time;
		public float endTime;

		public PlatformerTimer(float expirationTime)
		{
			endTime = expirationTime;
			hasEndTime = true;
			Timers.Add(this);
		}

		public PlatformerTimer()
		{
			hasEndTime = false;
			Timers.Add(this);
		}

		public static void ProcessTimers(float delta)
		{
			foreach(PlatformerTimer timer in Timers)
			{
				if(!timer.hasEndTime)
                    timer.time += 1 * delta;
				else
					if (!timer.Expired())
						timer.time += 1 * delta;
			}
		}

		public void Reset()
		{
			this.time = 0;
		}

		public bool Expired()
		{
			return endTime <= time;
		}
	}
}

