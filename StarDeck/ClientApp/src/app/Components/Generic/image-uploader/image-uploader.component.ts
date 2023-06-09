import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-image-uploader',
  templateUrl: './image-uploader.component.html',
  styleUrls: ['./image-uploader.component.scss']
})
export class ImageUploaderComponent {
  @Input() imageURL: string;

  constructor() {
    this.imageURL = '../../../assets/images/card.png';
  }

  onImageChanged() {
    /** Selecciona el input de la imagen */
    const fileInput: HTMLInputElement = document.querySelector('#file-input')!;
    /** Muestra la imagen seleccionada con el input */
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
