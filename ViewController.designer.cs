// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MoveOCR
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView ActivitySpinner { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AlphaNumericLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch AlphaNumericSwitch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView CameraView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView MainView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton OCRButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView OCRTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView PictureView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ResultLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider SelectionBarSlider { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView SelectionBarView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ActivitySpinner != null) {
                ActivitySpinner.Dispose ();
                ActivitySpinner = null;
            }

            if (AlphaNumericLabel != null) {
                AlphaNumericLabel.Dispose ();
                AlphaNumericLabel = null;
            }

            if (AlphaNumericSwitch != null) {
                AlphaNumericSwitch.Dispose ();
                AlphaNumericSwitch = null;
            }

            if (CameraView != null) {
                CameraView.Dispose ();
                CameraView = null;
            }

            if (MainView != null) {
                MainView.Dispose ();
                MainView = null;
            }

            if (OCRButton != null) {
                OCRButton.Dispose ();
                OCRButton = null;
            }

            if (OCRTextField != null) {
                OCRTextField.Dispose ();
                OCRTextField = null;
            }

            if (PictureView != null) {
                PictureView.Dispose ();
                PictureView = null;
            }

            if (ResultLabel != null) {
                ResultLabel.Dispose ();
                ResultLabel = null;
            }

            if (SelectionBarSlider != null) {
                SelectionBarSlider.Dispose ();
                SelectionBarSlider = null;
            }

            if (SelectionBarView != null) {
                SelectionBarView.Dispose ();
                SelectionBarView = null;
            }
        }
    }
}