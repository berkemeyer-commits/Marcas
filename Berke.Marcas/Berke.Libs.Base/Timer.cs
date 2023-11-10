using System;

namespace Berke.Libs.Base
{
	/// <summary>
	/// Summary description for Timer.
	/// </summary>
	public class Timer
	{
		#region Dato miembro
		long _ini;
		#endregion

		#region Constructores
		public Timer()
		{
			_ini = DateTime.Now.Ticks;
		}


		public Timer(DateTime initDateTime )
		{
			_ini = initDateTime.Ticks;
		}

		#endregion

		#region Seconds
		public float Seconds {
			get {
				return(float) ( DateTime.Now.Ticks - _ini) / (float) 10000000;
			}
		}
		#endregion

		#region AsString
		public string AsString 
		{
			get 
			{
				string ret ="";			
				float seg = (float) ( DateTime.Now.Ticks - _ini) / (float) 10000000;

				float dias = Convert.ToSingle( Math.Floor( seg / 60.0 / 60.0 / 8.0 ));

				seg = seg - Convert.ToSingle( dias *  60.0 / 60.0 / 8.0 );

				float horas =  Convert.ToSingle( Math.Floor( seg / 60.0 / 60.0 ));

				seg = seg - Convert.ToSingle( horas *  60.0 / 60.0 );

				
				float min =  Convert.ToSingle( Math.Floor( seg / 60.0 ));
				seg = seg - Convert.ToSingle( min *  60.0 );

				if( dias != 0.0 ) 
				{
					ret += " " + Convert.ToInt32( dias ).ToString();
					ret += "d. ";//dias == 1 ? " dia" : " dias";
				}

				if( horas != 0.0 )				{
					ret += " " + Convert.ToInt32( horas ).ToString();
					ret += "h. "; //horas == 1 ?  " hora" :  " horas";
				}
				if( min != 0.0 ) 
				{
					ret += " " + Convert.ToInt32( min ).ToString();
					ret += "m. ";// min == 1 ? " minuto" :" minutos";
				}
				if( seg != 0.0 ) 
				{
					ret += " " + ((float)(Math.Floor(seg * 100.0) / 100.0)).ToString();
					ret += "s. ";// seg == 1 ? "segundo" : " segundos";
				}

				return ret;
			}
		}
		#endregion

		#region Start
		public void Start()
		{
			_ini = DateTime.Now.Ticks;
		}
		#endregion


	}// end class Timer
}
