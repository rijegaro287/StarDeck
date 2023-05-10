import { Injectable } from '@angular/core';

import { CARD_TYPES } from '../app.component';

import { ICardType } from '../Interfaces/Card';

@Injectable({
  providedIn: 'root'
})
export class HelpersService {

  constructor() { }

  /**
   * Convierte un string en base 64 a una URL de imagen
   * @param base64 String en base 64
   * @returns String con URL de la imagen
   */
  base64ToImageURL(base64: string): string {
    return `data:image/png;base64,${base64}`;
  }

  /**
   * Convierte un archivo a un string en base64
   * @param file Archivo a convertir
   * @returns String en base64
   */
  async fileToBase64(file: File): Promise<string> {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    await new Promise<void>((resolve, reject) => reader.onload = () => resolve());

    return (reader.result as string).split(',')[1];
  }

  /**
   * Devuelve el color del borde de la carta según su tipo
   * @param type Tipo de la carta
   * @returns String con el color del borde
   */
  getCardBorderColor(type: ICardType): string {
    switch (type) {
      case CARD_TYPES.ULTRA_RARE:
        return '#e6ce4a';
      case CARD_TYPES.VERY_RARE:
        return '#9a00d6';
      case CARD_TYPES.RARE:
        return '#4161f1';
      case CARD_TYPES.NORMAL:
        return '#66e961';
      case CARD_TYPES.BASIC:
        return 'black';
      default:
        return 'black';
    }
  }
}
