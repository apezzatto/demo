using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seabird
{
    class Program
    {
        static void Main(string[] args)
        {
            //Observers
            ControlPanel monitor1 = new Monitor();
            ControlPanel monitor2 = new Monitor();
            ControlPanel monitor3 = new Monitor();

            // No adapter
            Console.WriteLine("Experiment 1: test the aircraft engine");
            IAircraft aircraft = new Aircraft();

            //Attaching the observer to the subject
            aircraft.Attach(monitor1);

            aircraft.TurnEngineOn();
            aircraft.Accelerate(50);
            aircraft.Deaccelerate(0);
            aircraft.Accelerate(200);
            aircraft.RaiseNose(40);
            aircraft.TakeOff();
            aircraft.DownNose(0);
            aircraft.Deaccelerate(100);
            aircraft.DownNose(-30);
            aircraft.Land();
            aircraft.Deaccelerate(0);

            //Showing the notifications
            Console.WriteLine(monitor1.Message);

            // Classic usage of an adapter
            Console.WriteLine("\nExperiment 2: Use the engine in the Seabird");
            IAircraft seabird = new Seabird();
            
            //Attaching the observer to the subject
            seabird.Attach(monitor2);

            seabird.TurnEngineOn();
            seabird.Accelerate(40);
            seabird.Deaccelerate(0);
            seabird.Accelerate(190);
            seabird.RaiseNose(50);
            seabird.TakeOff();
            seabird.DownNose(0);
            seabird.Deaccelerate(120);
            seabird.DownNose(-20);
            seabird.Land();
            seabird.Deaccelerate(0);

            //Showing the notifications
            Console.WriteLine(monitor2.Message);

            // Two-way adapter: using seacraft instructions on an IAircraft object
            // (where they are not in the IAircraft interface)
            Console.WriteLine("\nExperiment 3: Increase the speed of the Seabird:");
            
            //Attaching the observer to the subject
            (seabird as ISeacraft).Attach(monitor3);
            (seabird as ISeacraft).IncreaseRevs(500);
            
            //Showing the notifications
            Console.WriteLine(monitor3.Message);

            Console.WriteLine("Seabird flying at height " + seabird.Height + " meters and speed " + (seabird as ISeacraft).Knots + " knots");
            Console.WriteLine("Experiments successful; the Seabird flies!");

            Console.ReadLine();

            monitor1 = null;
            monitor2 = null;
            monitor3 = null;
            aircraft = null;
            seabird = null;
        }
    }

    // Two-Way Adapter Pattern  Pierre-Henri Kuate and Judith Bishop  Aug 2007
    // Embedded system for a Seabird flying plane

    /// <summary>
    /// Abstract Observer
    /// </summary>
    public abstract class ControlPanel
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public abstract void Update(string message);
    }

    /// <summary>
    /// Concrete Observer
    /// </summary>
    public class Monitor : ControlPanel
    {
        public Monitor() { }

        public override void Update(string message)
        {
            base.Message += message + Environment.NewLine;
        }
    }

    /// <summary>
    /// Adapter Pattern: Target Interface
    /// Observer Pattern: Subject Interface
    /// </summary>
    public interface IAircraft
    {
        //Properties should be declared first, then the methods
        bool IsEngineTurnedOn { get; }

        int Height { get; }
        int Speed { get; }
        int NoseDegree { get; }
        string CockpitMessage { get; }
        void TurnEngineOn();
        void TurnEngineOff();
        void TakeOff();
        void Accelerate(int speed);
        void Deaccelerate(int speed);
        void RaiseNose(int degree);
        void DownNose(int degree);
        void Land();

        //Observer Design Pattern implementation
        void Attach(ControlPanel observer);
        void Detach(ControlPanel observer);
    }

    /// <summary>
    /// Adapter Pattern: Target
    /// Observer Pattern: Concrete Subject
    /// </summary>
    public sealed class Aircraft : IAircraft
    {
        private const int MAX_ALTITUDE = 300;
        private const int TAKE_OFF_SPEED = 200;
        private const int TAKE_OFF_NOSE_DEGREE = 80;

        //By convention, fields should start with "_"
        private bool _isEngineTurnedOn;
        private int _noseDegree;
        private int _height;
        private int _speed;
        private string _cockpitMessage;
        private List<ControlPanel> _monitors;

        //Properties first, methods later
        public int Height
        {
            get { return _height; }
        }

        public int Speed
        {
            get { return _speed; }
        }

        public int NoseDegree
        {
            get { return _noseDegree; }
        }

        public bool IsEngineTurnedOn
        {
            get { return _isEngineTurnedOn; }
        }

        public string CockpitMessage
        {
            get { return _cockpitMessage; }
        }

        public Aircraft()
        {
            _isEngineTurnedOn = false;
            _noseDegree = 0;
            _height = 0;
            _speed = 0;
            _monitors = new List<ControlPanel>();
            _cockpitMessage = string.Empty;
        }

        public void Attach(ControlPanel observer)
        {
            _monitors.Add(observer);
        }

        public void Detach(ControlPanel observer)
        {
            _monitors.Remove(observer);
        }

        public void TurnEngineOn()
        {
            _isEngineTurnedOn = true;
            this.Notify("Engine turned on");
        }

        public void TurnEngineOff()
        {
            _isEngineTurnedOn = false;
            this.Notify("Engine turned off");
        }

        private void Notify(string message)
        {
            foreach (ControlPanel cp in _monitors)
                cp.Update(message);
        }

        public void Accelerate(int speed)
        {
            if (!_isEngineTurnedOn)
                this.Notify("Engine is not turned on");
            else
            {
                //If the current speed is bigger than the new speed request, keep the same speed.
                if (_speed < speed)
                {
                    for (int i = 0; i <= speed; i += 10)
                    {
                        _speed = i;
                        this.Notify("Accelarating to " + _speed + " Km/h");
                    }
                }
            }
        }

        public void Deaccelerate(int speed)
        {
            if (!_isEngineTurnedOn)
                this.Notify("Engine is not turned on");
            else
            { 
                //If the current speed is less than the new speed request, keep the same speed.
                if (_speed > speed)
                {
                    for (int i = _speed; i >= speed; i -= 10)
                    {
                        _speed = i;
                        this.Notify("Decelarating to " + _speed + " Km/h");
                    }

                    if (speed.Equals(0))
                        this.Notify("The plane has stoped");
                }
            }
        }

        public void RaiseNose(int degree)
        {
            int dgr = degree;

            //If the degree requested is bigger than the max degree allowed, assign the max degree allowed.
            if (degree > TAKE_OFF_NOSE_DEGREE)
                dgr = TAKE_OFF_NOSE_DEGREE;

            for (int i = 0; i <= dgr; i += 10)
            {
                _noseDegree = i;
                this.Notify("Raising nose to " + _noseDegree + " degrees");
            }
        }

        public void DownNose(int degree)
        {
            //Stabilizing nose
            for (int i = _noseDegree; i >= degree; i -= 10)
            {
                _noseDegree = i;
                this.Notify("Putting the nose down to " + _noseDegree + " degrees");
            }
        }

        public void TakeOff()
        {
            bool check = true;

            #region Checks before take off
            if (!_isEngineTurnedOn)
            {
                this.Notify("Engine is not turned on");
                check = false;
            }
            #endregion

            //If all checks are fine, take off.
            if (check)
            {
                this.Notify("Taking off");
                for (int i = 0; i <= MAX_ALTITUDE; i += 10)
                {
                    _height = i;
                    this.Notify("Altitude " + _height);
                }
            }
            else
                this.Notify("Take off procedures aborted.");
        }

        public void Land()
        {
            if (_height.Equals(0))
                this.Notify("The plane is already landed");
            else
            {
                this.Notify("Starting landing procedures");
                for (int i = _height; i >= 0; i -= 10)
                {
                    _height = i;
                    this.Notify("Altitude " + _height + " meters");
                }
                this.Notify("Landing procedures successfully completed");
            }
        }
    }

    /// <summary>
    /// Adapter Pattern: Adptee Interface
    /// Observer Pattern: Subject Interface
    /// </summary>
    public interface ISeacraft
    {
        int Knots { get; }
        void IncreaseRevs(int knots);
        
        //Observer Design Pattern
        void Attach(ControlPanel observer);
        void Detach(ControlPanel observer);
    }

    // Adaptee implementation
    /// <summary>
    /// Adapter Pattern: Adptee implementation
    /// Observer Pattern: Concrete Subject
    /// </summary>
    public class Seacraft : ISeacraft
    {
        const int MAX_KNOTS = 500;

        private List<ControlPanel> _monitors = new List<ControlPanel>();

        int _knots = 0;

        public int Knots
        {
            get { return _knots; }
        }

        public void Attach(ControlPanel observer)
        {
            _monitors.Add(observer);
        }

        public void Detach(ControlPanel observer)
        {
            _monitors.Remove(observer);
        }

        public virtual void IncreaseRevs(int knots)
        {
            //If the current knot is bigger than the new knot request, keep the same knot.
            if (_knots < knots)
            {
                for (int i = 0; i < knots; i += 100)
                {
                    _knots = i;
                    this.Notify("Increasing engine revolution to " + _knots + " knots");
                }
            }
        }

        public void Notify(string message)
        {
            foreach (ControlPanel cp in _monitors)
                cp.Update(message);
        }
    }

    /// <summary>
    /// Adapter Pattern: Adapter
    /// Observer Pattern: Subject
    /// </summary>
    public class Seabird : Seacraft, IAircraft
    {
        // A two-way adapter hides and routes the Target's methods
        // Use Seacraft instructions to implement this one
        private const int MAX_ALTITUDE = 300;
        private const int TAKE_OFF_SPEED = 300;
        private const int TAKE_OFF_NOSE_DEGREE = 80;

        //By convention, fields should start with "_"
        private bool _isEngineTurnedOn;
        private int _noseDegree;
        private int _height;
        private int _speed;
        private string _cockpitMessage;
        //private List<ControlPanel> _monitors;

        //Properties first, methods later
        public int Height
        {
            get { return _height; }
        }

        public int Speed
        {
            get { return _speed; }
        }

        public bool IsEngineTurnedOn
        {
            get { return _isEngineTurnedOn; }
        }

        public int NoseDegree
        {
            get { return _noseDegree; }
        }

        public string CockpitMessage
        {
            get { return _cockpitMessage; }
        }

        public Seabird()
        {
            _isEngineTurnedOn = false;
            _noseDegree = 0;
            _height = 0;
            _speed = 0;
            //_monitors = new List<ControlPanel>();
            _cockpitMessage = string.Empty;
        }

        public void TurnEngineOn()
        {
            _isEngineTurnedOn = true;
            this.Notify("Engine turned on");
        }

        public void TurnEngineOff()
        {
            _isEngineTurnedOn = false;
            this.Notify("Engine turned off");
        }

        public void Accelerate(int speed)
        {
            if (!_isEngineTurnedOn)
                this.Notify("Engine is not turned on");
            else
            {
                //If the current speed is bigger than the new speed request, keep the same speed.
                if (_speed < speed)
                {
                    for (int i = 0; i <= speed; i += 10)
                    {
                        _speed = i;
                        this.Notify("Accelarating to " + _speed + " Km/h");
                    }
                }
            }
        }

        public void Deaccelerate(int speed)
        {
            if (!_isEngineTurnedOn)
                this.Notify("Engine is not turned on");
            else
            {
                //If the current speed is less than the new speed request, keep the same speed.
                if (_speed > speed)
                {
                    for (int i = _speed; i >= speed; i -= 10)
                    {
                        _speed = i;
                        this.Notify("Decelarating to " + _speed + " Km/h");
                    }

                    if (speed.Equals(0))
                        this.Notify("The plane has stoped");
                }
            }
        }

        public void RaiseNose(int degree)
        {
            int dgr = degree;

            //If the degree requested is bigger than the max degree allowed, assign the max degree allowed.
            if (degree > TAKE_OFF_NOSE_DEGREE)
                dgr = TAKE_OFF_NOSE_DEGREE;

            for (int i = 0; i <= dgr; i += 10)
            {
                _noseDegree = i;
                this.Notify("Raising nose to " + _noseDegree + " degrees");
            }
        }

        public void DownNose(int degree)
        {
            //Stabilizing nose
            for (int i = _noseDegree; i >= degree; i -= 10)
            {
                _noseDegree = i;
                this.Notify("Putting the nose down to " + _noseDegree + " degrees");
            }
        }

        public void TakeOff()
        {
            bool check = true;

            #region Checks before take off
            if (!_isEngineTurnedOn)
            {
                this.Notify("Engine is not turned on");
                check = false;
            }
            #endregion

            //If all checks are fine, take off.
            if (check)
            {
                this.Notify("Taking off");
                for (int i = 0; i <= MAX_ALTITUDE; i += 10)
                {
                    _height = i;
                    this.Notify("Altitude " + _height);
                }
            }
            else
                this.Notify("Take off procedures aborted.");
        }

        public void Land()
        {
            if (_height.Equals(0))
                this.Notify("The plane is already landed");
            else
            {
                this.Notify("Starting landing procedures");
                for (int i = _height; i >= 0; i -= 10)
                {
                    _height = i;
                    this.Notify("Altitude " + _height + " meters");
                }
                this.Notify("Landing procedures successfully completed");
            }
        }

        // This method is common to both Target and Adaptee
        public override void IncreaseRevs(int knots)
        {
            base.IncreaseRevs(knots);
        }
    }
}
/* Output
Experiment 1: test the aircraft engine
Aircraft engine turned on.
Aircraft speed: 0Km/h
Aircraft speed: 50Km/h
Aircraft speed: 100Km/h
Aircraft speed: 150Km/h
Aircraft speed: 200Km/h
Aircraft nose degree: 0 degrees
Aircraft nose degree: 20 degrees
Aircraft nose degree: 40 degrees
Aircraft nose degree: 60 degrees
Aircraft nose degree: 80 degrees
Aircraft altitude: 0
Aircraft altitude: 50
Aircraft altitude: 100
Aircraft altitude: 150
Aircraft altitude: 200
Aircraft altitude: 250
Aircraft altitude: 300
Aircraft altitude: 350
Aircraft altitude: 400
Aircraft altitude: 450
Aircraft altitude: 500
Aircraft nose degree: 80 degrees
Aircraft nose degree: 60 degrees
Aircraft nose degree: 40 degrees
Aircraft nose degree: 20 degrees
Aircraft nose degree: 0 degrees
The aircraft is flying

Experiment 2: Use the engine in the Seabird
Seabird engine turned on.
Seabird speed: 0Km/h
Seabird speed: 50Km/h
Seabird speed: 100Km/h
Seabird speed: 150Km/h
Seabird speed: 200Km/h
Seabird nose degree: 0 degrees
Seabird nose degree: 20 degrees
Seabird nose degree: 40 degrees
Seabird nose degree: 60 degrees
Seabird altitude: 0
Seabird altitude: 50
Seabird altitude: 100
Seabird altitude: 150
Seabird altitude: 200
Seabird altitude: 250
Seabird altitude: 300
Seabird nose degree: 60 degrees
Seabird nose degree: 40 degrees
Seabird nose degree: 20 degrees
Seabird nose degree: 0 degrees
The Seabird took off

Experiment 3: Increase the speed of the Seabird:
Seacraft engine increases revs to 0 knots
Seacraft engine increases revs to 100 knots
Seacraft engine increases revs to 200 knots
Seacraft engine increases revs to 300 knots
Seacraft engine increases revs to 400 knots
Seabird flying at height 300 meters and speed 400 knots
Experiments successful; the Seabird flies!
*/
