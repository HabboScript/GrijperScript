using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Sulakore.Communication;
using Sulakore.Modules;
using Sulakore.Protocol;
using Tangine;

namespace GrijperScript
{
    [Module("GrijperScript", "")]
    [Author("JustYonas")]
    public partial class GrijperScript : ExtensionForm
    {
        #region fields
        private ushort useFurnitureID = 0;
        int countMove = 0;
        private Location location0 = new Location();
        private Location location1 = new Location();
        private Location locationPlayerTile0 = new Location();
        private Location locationPlayerTile1 = new Location();
        private Location lastMove0 = new Location();
        private Location lastMove1 = new Location();
        private int lastInt1 = 0;
        #endregion;

        public GrijperScript()
        {
            InitializeComponent();
            AllocConsole();
            UpdateStatus();
            Console.WriteLine("Initialized ctor");
        }

        #region triggers 
        private void OnFurnitureUpdate(DataInterceptedEventArgs e)
        {
            int fromX = e.Packet.ReadInteger();
            int fromY = e.Packet.ReadInteger();
            int toX = e.Packet.ReadInteger();
            int ToY = e.Packet.ReadInteger();
            e.Packet.ReadInteger();
            int itemId = e.Packet.ReadInteger();
            CheckOnRollerUpdate(toX, ToY, itemId);
            UpdateStatus();
        }

        private void onMoveRecieved(DataInterceptedEventArgs e)
        {
            int toX = e.Packet.ReadInteger();
            int toY = e.Packet.ReadInteger();
            Console.WriteLine($"MoveAvatar: x: {toX}, y: {toY} ");

            switch (_ScriptState)
            {
                case ScriptState.SelectCorner:
                    {
                        _config.Corner.X = toX;
                        _config.Corner.Y = toY;
                        _ScriptState = ScriptState.none;
                        break;
                    }
            }
            UpdateStatus();
        }
        private void Switch()
        {
            //{l}{u:1294}{i:274071115}{i:0}
            HMessage message = new HMessage(useFurnitureID);
            message.WriteInteger(_config.SwitchId);
            message.WriteInteger(0);
            this.Connection.SendToServerAsync(message);
        }
        #endregion

        #region Calculations
        private void OnTriggerRecieved(DataInterceptedEventArgs e)
        {
            int itemID = e.Packet.ReadInteger();
            int state = e.Packet.ReadInteger();
            Console.WriteLine($"UseFurniture: {itemID}");

            switch (_ScriptState)
            {
                case ScriptState.selectSwitch:
                    _config.SwitchId = itemID;
                    _ScriptState = ScriptState.none;
                    break;
                case ScriptState.selectTegel0:
                    _config.PlayerTile0 = itemID;
                    _ScriptState = ScriptState.none;
                    break;
                case ScriptState.selectTegel1:
                    _config.PlayerTile1 = itemID;
                    _ScriptState = ScriptState.none;
                    break;
            }
            UpdateStatus();
        }

        private void CheckOnRollerUpdate(int x, int y, int itemId)
        {
            int fromCornerX = x - _config.Corner.X;
            int fromCornerY = y - _config.Corner.Y;
            if (fromCornerX != 0 && fromCornerY != 0 ||
                fromCornerX < 0 || fromCornerY < 0 ||
                fromCornerX > 6 || fromCornerY > 6)
            {
                return;
            }
            if (itemId == _config.PlayerTile0 || itemId == _config.PlayerTile1)
            {
                if (itemId == _config.PlayerTile0)
                {
                    locationPlayerTile0.X = fromCornerX;
                    locationPlayerTile0.Y = fromCornerY;
                }

                if (itemId == _config.PlayerTile1)
                {
                    locationPlayerTile1.X = fromCornerX;
                    locationPlayerTile1.Y = fromCornerY;
                }
                return;
            }
            else
            {
                if (countMove++ == 0)
                {
                    location0.X = fromCornerX;
                    location0.Y = fromCornerY;
                    Console.WriteLine($"Setting loc0 to: x: {fromCornerX}, y: {fromCornerY} ");
                    new Thread(CheckState).Start();
                }
                else
                {
                    countMove = 0;
                    location1.X = fromCornerX;
                    location1.Y = fromCornerY;
                    Console.WriteLine($"Setting loc1 to: x: {fromCornerX}, y: {fromCornerY} ");
                    new Thread(CheckState).Start();
                }
            }
            Console.WriteLine($"moving to: x: {fromCornerX}, y: {fromCornerY} ");
        }

        private void CheckState()
        {
            var k = DateTime.Now.Millisecond;
            lastInt1 = k;
            Thread.Sleep(150);
            if (lastInt1 == k)
            {
                switch (_ScriptState)
                {
                    case ScriptState.grijpenAuto:
                        AutoSelect(_config.Hit);
                        break;

                }
            }
        }
        private void AutoSelect(bool raak)
        {
            if (IsHit())
            {
                Console.WriteLine($"Autoselect->hit");
                Switch();
                lastMove0 = new Location(location0.X, location0.Y);
                lastMove1 = new Location(location1.X, location1.Y);
                _ScriptState = ScriptState.none;
            }
            else
            {
                Console.WriteLine($"Autoselect->Miss");
            }
        }

        private bool IsHit()
        {
            return
           CheckTile(location0, locationPlayerTile0) ||
           CheckTile(location0, locationPlayerTile1) ||
           CheckTile(location1, locationPlayerTile0) ||
           CheckTile(location1, locationPlayerTile1);
        }

        private bool CheckTile(Location location, Location location1)
        {
            return HitPlaces.Any(o => (o.X == location.X && o.Y == location1.Y) || (o.X == location1.X && o.Y == location.Y));
        }

        #endregion

        #region initialization
        private void initializeTriggers()
        {
            useFurnitureID = this.Out.GetId("UseFurniture");
            var moveAvatarId = this.Out.GetId("MoveAvatar");
            var objectOnRollerId = this.In.GetId("ObjectOnRoller");

            this.Triggers.OutAttach(useFurnitureID, (e) => OnTriggerRecieved(e));
            this.Triggers.OutAttach(moveAvatarId, (e) => onMoveRecieved(e));
            this.Triggers.InAttach(objectOnRollerId, (e) => OnFurnitureUpdate(e));

        }

        private void UpdateStatus()
        {
            var newline = Environment.NewLine;
            var methode = _config.Hit ? "raak" : "mis";
            var t =
                $"Status: {_ScriptState} {newline} " +
                $"hendelid: {_config.SwitchId} {newline} " +
                $"hoek: {_config.Corner.X},{_config.Corner.Y} {newline} " +
                $"Methode: {methode} {newline}" +
                $"currentLoc: {location0.X},{location0.Y} - {location1.X},{location1.Y} {newline} " +
                $"playerTegels: {_config.PlayerTile0},{_config.PlayerTile1} {newline} " +
                $"playerTegelsLoc: {locationPlayerTile0.X},{locationPlayerTile0.Y} - {locationPlayerTile1.X},{locationPlayerTile1.Y} {newline} " +
                $"LastMove: {lastMove0.X},{lastMove0.Y}  - {lastMove1.X},{lastMove1.Y}" +

                "";
            statusmessage.Text = t;
        }

        public override void HandleIncoming(DataInterceptedEventArgs e)
        {
            Connected = true;
            base.HandleIncoming(e);
        }

        #endregion

        #region logging
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        #endregion

        #region properties and stuff
        private Config _config = new Config();
        private ScriptState _ScriptState = ScriptState.none;

        public List<Location> HitPlaces = new List<Location>()
            {
                     new Location(1,2),
                     new Location(1,4),
                     new Location(2,1),
                     new Location(2,5),
                     new Location(3,3),
                     new Location(3,4),
                     new Location(3,6),
                     new Location(4,1),
                     new Location(4,3),
                     new Location(4,4),
                     new Location(5,2),
                     new Location(5,6),
                     new Location(6,3),
                     new Location(6,5),
                     new Location(6,6)
            };

        private bool _connected;
        public bool Connected
        {
            get { return _connected; }
            set
            {
                if (value && !_connected)
                {
                    initializeTriggers();
                }
                _connected = value;
            }
        }




        #endregion

        #region buttons
        private void SelectHendel_Click(object sender, EventArgs e)
        {
            _ScriptState = ScriptState.selectSwitch;
            UpdateStatus();
        }

        private void SelectHoek_Click(object sender, EventArgs e)
        {
            _ScriptState = ScriptState.SelectCorner;
            UpdateStatus();
        }

        private void StartGrijpAuto_Click(object sender, EventArgs e)
        {
            _ScriptState = ScriptState.grijpenAuto;
            location0 = new Location();
            location1 = new Location();
            UpdateStatus();
        }


        private void SelectTegel0_Click(object sender, EventArgs e)
        {
            _ScriptState = ScriptState.selectTegel0;
            UpdateStatus();
        }

        private void SelectTegel1_Click(object sender, EventArgs e)
        {
            _ScriptState = ScriptState.selectTegel1;
            UpdateStatus();
        }

        private void SetMehod_Click(object sender, EventArgs e)
        {
            _config.Hit = !_config.Hit;
            UpdateStatus();
        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JSON|*.json";
            saveFileDialog1.Title = "Save the settings";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                var jsonSave = JsonConvert.SerializeObject(_config);
                File.WriteAllText(saveFileDialog1.FileName, jsonSave);
            }
        }

        private void LoadSave_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON|*.json";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName != "")
                {
                    var settingString = File.ReadAllText(openFileDialog.FileName);
                    _config = JsonConvert.DeserializeObject<Config>(settingString);
                }
            }
        }



        #endregion
    }


}
