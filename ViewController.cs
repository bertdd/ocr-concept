using System;
using System.Text;
using System.Threading.Tasks;
using AVFoundation;
using CoreGraphics;
using Tesseract.iOS;
using UIKit;
using Xamarin.Essentials;
using static System.Math;

namespace MoveOCR
{
  public partial class ViewController : UIViewController
  {
    public ViewController(IntPtr handle) : base(handle) { }


    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
      OCR = new TesseractApi();

      CaptureSession = new AVCaptureSession();
      ImageOutput = new AVCapturePhotoOutput();

      var cameraDevice = AVCaptureDevice.GetDefaultDevice(AVMediaType.Video);
      var cameraInput = AVCaptureDeviceInput.FromDevice(cameraDevice);

      CaptureSession.AddInput(cameraInput);
      CaptureSession.AddOutput(ImageOutput);

      SetupUI();
      CaptureSession.StartRunning();

      Camera = new CameraHandler();
      Camera.FinishedProcessing += async delegate
      {
        PictureView.Image = new UIImage(Camera.Picture, 1f, UIImageOrientation.Right);
        Capture = PictureView.Capture();
        await InitReader();
      };

      OCRButton.TouchUpInside += async delegate
      {
        HandleButtonClick();
      };

      AlphaNumericSwitch.ValueChanged += async delegate
      {
        await SetOcrTextLabel();
      };

      // Selection slider Setup
      SelectionBarSlider.TouchUpInside += async delegate
      {
        await InitReader();
      };

      SelectionBarSlider.TouchUpOutside += async delegate
      {
        await InitReader();
      };

      SelectionBarSlider.ValueChanged += delegate
      {
        var tempFrame = SelectionBarView.Frame;
        tempFrame.Y = (SelectionBarSlider.Value * 92) + 22;
        SelectionBarView.Frame = tempFrame;
      };
    }


    public void SetupUI()
    {
      // Live Camera Feed
      var layer = new AVCaptureVideoPreviewLayer(CaptureSession);
      layer.VideoGravity = AVLayerVideoGravity.ResizeAspectFill;

      layer.Frame = CameraView.Bounds;
      CameraView.Layer.AddSublayer(layer);

      // Selection Bar Options
      SelectionBarView.Layer.BorderWidth = 3.5f;
      SelectionBarView.Layer.BorderColor = new CGColor(246, 137, 27, 0.35f);

      SelectionBarSlider.Transform = CGAffineTransform.MakeRotation((float)PI / 2);
    }


    void HandleButtonClick()
    {
      ToggleBusy(true);
      if (OCRButton.TitleLabel.Text == "Take Picture")
      {
        using (var settings = AVCapturePhotoSettings.Create())
        {
          ImageOutput.CapturePhoto(settings, Camera);
        }
        OCRButton.SetTitle("Retake Picture", UIControlState.Normal);
      }
      else
      {
        PictureView.Image = null;
        Capture = null;
        OCRButton.SetTitle("Take Picture", UIControlState.Normal);
        ToggleBusy(false);
      }
    }


    async Task InitReader()
    {
      ToggleBusy(true);
      if (!OCR.Initialized)
      {
        await OCR.Init("eng");
      }

      if (Capture != null)
      {
        var cropped = CropCapture();
        await OCR.SetImage(cropped.AsJPEG().ToArray());

        Result = OCR.Text;
        FilteredResult = FilterAlphaNumeric(OCR.Text);

        await SetOcrTextLabel();
      }
      ToggleBusy(false);
    }


    UIImage CropCapture()
    {
      var rect = new CGRect(new CGPoint(0, 0), SelectionBarView.Frame.Size);
      var location = new CGPoint(0, 22 - SelectionBarView.Frame.Y);

      UIGraphics.BeginImageContextWithOptions(rect.Size, false, Capture.CurrentScale);
      Capture.Draw(location);
      var cropped = UIGraphics.GetImageFromCurrentImageContext();
      UIGraphics.EndImageContext();
      return cropped;
    }


    string FilterAlphaNumeric(string text)
    {
      var result = new StringBuilder();
      foreach (var ch in text)
      {
        if (char.IsLetterOrDigit(ch))
        {
          result.Append(ch);
        }
      }

      return result.ToString();
    }


    async Task SetOcrTextLabel()
    {
      var result = AlphaNumericSwitch.On ? FilteredResult : Result;
      if (!string.IsNullOrEmpty(result))
      {
        OCRTextField.Text = result;
        await Clipboard.SetTextAsync(result);
      }
    }


    void ToggleBusy(bool busy)
    {
      OCRButton.Enabled = !busy;
      ActivitySpinner.Hidden = !busy;
    }


    string Result;
    string FilteredResult;
    UIImage Capture;

    TesseractApi OCR;
    CameraHandler Camera;
    AVCaptureSession CaptureSession;
    AVCapturePhotoOutput ImageOutput;
  }
}
