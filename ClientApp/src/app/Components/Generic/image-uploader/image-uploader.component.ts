import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-image-uploader',
  templateUrl: './image-uploader.component.html',
  styleUrls: ['./image-uploader.component.css']
})
export class ImageUploaderComponent {
  @Input() imageURL: string;

  constructor() {
    this.imageURL = '../../../assets/images/card.png';
  }

  onImageChanged() {
    const fileInput: HTMLInputElement = document.querySelector('#file-input')!;
    const inputImage: HTMLImageElement = document.querySelector('#input-image')!;

    let newImageURL = this.imageURL

    if (fileInput && fileInput.files) {
      newImageURL = URL.createObjectURL(fileInput.files[0])
    }

    if (inputImage) {
      inputImage.src = newImageURL;
    }
  }
}
