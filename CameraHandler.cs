using System;
using AVFoundation;
using CoreGraphics;
using Foundation;

namespace MoveOCR
{
  public class CameraHandler : AVCapturePhotoCaptureDelegate
  {
    public override void DidFinishProcessingPhoto(AVCapturePhotoOutput output, AVCapturePhoto photo, NSError error)
    {
      Picture = photo.CGImageRepresentation;
      FinishedProcessing?.Invoke(this, null);
    }

    public CGImage Picture;
    public event EventHandler<EventArgs> FinishedProcessing;
  }
}
