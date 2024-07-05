using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace RawInput.Touchpad
{
	public partial class MainWindow : Window
	{
		public bool TouchpadExists
		{
			get { return (bool)GetValue(TouchpadExistsProperty); }
			set { SetValue(TouchpadExistsProperty, value); }
		}
		public static readonly DependencyProperty TouchpadExistsProperty =
			DependencyProperty.Register("TouchpadExists", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

		public string TouchpadContacts
		{
			get { return (string)GetValue(TouchpadContactsProperty); }
			set { SetValue(TouchpadContactsProperty, value); }
		}
		public static readonly DependencyProperty TouchpadContactsProperty =
			DependencyProperty.Register("TouchpadContacts", typeof(string), typeof(MainWindow), new PropertyMetadata(null));

		public MainWindow()
        {
			InitializeComponent();
            webView.NavigationCompleted += OnTouchpadContactsChanged;
            webView.WebMessageReceived += WebView_WebMessageReceived;
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string indexPath = Path.Combine(currentDir, "index.html"); // Assuming the HTML is in the root

            // Construct a URI pointing to the local HTML file:
            Uri localHtmlUri = new Uri($"file:///{indexPath.Replace("\\", "/")}");

            // Navigate the WebView2 to the file URI:
            webView.Source = localHtmlUri;
            // เริ่มการตรวจจับการเปลี่ยนแปลงของ TouchpadContacts
            DependencyPropertyDescriptor.FromProperty(TouchpadContactsProperty, typeof(MainWindow))
                .AddValueChanged(this, OnTouchpadContactsChanged);
        }



        private void OnTouchpadContactsChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TouchpadContacts))
            {
                
                try
                {
                    // ใช้ Regular Expression ในการแยกข้อมูล
                    var regex = new Regex(@"Contact ID:(\d+) Point:(\d+),(\d+)");
                    var matches = regex.Matches(TouchpadContacts);

                    // สร้าง List ของ Object สำหรับเก็บข้อมูล
                    var contactsList = new List<object>();

                    foreach (Match match in matches)
                    {
                        int id = int.Parse(match.Groups[1].Value);
                        int x = int.Parse(match.Groups[2].Value);
                        int y = int.Parse(match.Groups[3].Value);

                        contactsList.Add(new { ID = id, X = x, Y = y });
                    }

                    // แปลง List เป็น JSON string
                    var dataObject = new { data = contactsList };
                    //string jsonContacts = JsonConvert.SerializeObject(contactsList);
                    string jsonContacts = JsonConvert.SerializeObject(dataObject);

                    //string jsonContacts = string.Join(",", dataObject.Select(JsonConvert.SerializeObject));
                    // แสดงค่า jsonContacts ใน Output Window
                    Debug.WriteLine("jsonContacts: " + jsonContacts);

                    // ส่งข้อมูล JSON ไปยัง WebView2
                    webView.CoreWebView2.PostWebMessageAsJson(jsonContacts);
                }
                catch (Exception ex)
                {
                    // จัดการข้อผิดพลาด (เช่น แสดงข้อความเตือน)
                    MessageBox.Show("Error processing TouchpadContacts: " + ex.Message);

                    // แสดงข้อผิดพลาดใน Output Window
                    Debug.WriteLine("Error: " + ex.Message);
                }
            }
        }


        //new
        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                string newText = "This text is set from C#!";
                string jsonData = $"{{\"message\": \"{newText}\"}}";
                webView.CoreWebView2.PostWebMessageAsJson(jsonData);
            }
        }

        private void WebView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string responseMessage = e.WebMessageAsJson;
            // Process the response message from JavaScript here
        }
		//new

        private HwndSource _targetSource;
		private readonly List<string> _log = new();

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			_targetSource = PresentationSource.FromVisual(this) as HwndSource;
			_targetSource?.AddHook(WndProc);

			TouchpadExists = TouchpadHelper.Exists();

			_log.Add($"Precision touchpad exists: {TouchpadExists}");

			if (TouchpadExists)
			{
				var success = TouchpadHelper.RegisterInput(_targetSource.Handle);

				_log.Add($"Precision touchpad registered: {success}");
			}
		}

		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			switch (msg)
			{
				case TouchpadHelper.WM_INPUT:
					var contacts = TouchpadHelper.ParseInput(lParam);
					TouchpadContacts = string.Join(Environment.NewLine, contacts.Select(x => x.ToString()));

					_log.Add("---");
					_log.Add(TouchpadContacts);
					break;
			}
			return IntPtr.Zero;
		}
	}
}