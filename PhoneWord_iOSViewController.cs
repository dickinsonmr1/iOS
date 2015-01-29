using System;
using System.Drawing;
using System.Collections.Generic;

using Foundation;
using UIKit;

// http://developer.xamarin.com/guides/ios/getting_started/hello,_iOS_multiscreen/hello,_iOS_multiscreen_deepdive/

namespace PhoneWord_iOS
{
	public partial class PhoneWord_iOSViewController : UIViewController
	{
		string translatedNumber = String.Empty;
		public List<String> PhoneNumbers { get; set;}

		public PhoneWord_iOSViewController (IntPtr handle) : base (handle)
		{
			PhoneNumbers = new List<String> ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TranslateButton.TouchUpInside += (object sender, EventArgs e) => {
				translatedNumber = Core.PhonewordTranslator.ToNumber (PhoneNumberText.Text);
				PhoneNumberText.ResignFirstResponder ();

				if (translatedNumber == string.Empty) {
					CallButton.SetTitle ("Call", UIControlState.Normal);
					CallButton.Enabled = false;
				} else {
					CallButton.SetTitle ("Call " + translatedNumber, UIControlState.Normal);
					CallButton.Enabled = true;
				}
			};

			CallButton.TouchUpInside += (object sender, EventArgs e) => {
				PhoneNumbers.Add(translatedNumber);

				var url = new NSUrl ("tel:" + translatedNumber);

				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					var av = new UIAlertView ("Not supported", "Scheme 'tel:' is not supported on this device", null, "OK", null);
					av.Show ();
				}					
			};

			CallHistoryButton.TouchUpInside += (object sender, EventArgs e) => 
			{
				CallHistoryController callHistory = this.Storyboard.InstantiateViewController("CallHistoryController") as CallHistoryController;
				if(callHistory != null)
				{
					callHistory.PhoneNumbers = PhoneNumbers;
					this.NavigationController.PushViewController (callHistory, true);
				}
			};
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

//		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
//		{
//			base.PrepareForSegue (segue, sender);
//
//			// set the view controller that's powering the screen we're transitioning to
//			var callHistoryController = segue.DestinationViewController as CallHistoryController;
//
//			// set the table view controller's list of phone number to the list of dialed phone numbers
//
//			if (callHistoryController != null)			
//			{
//				callHistoryController.PhoneNumbers = PhoneNumbers;
//			}
//		}



		#endregion
	}
}

