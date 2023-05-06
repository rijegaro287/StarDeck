import { Injectable } from '@angular/core';

import { ICardType } from '../Interfaces/Card';

import { IPlanetType } from '../Interfaces/Planet';

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
      case 4:
        return '#e6ce4a';
      case 3:
        return '#9a00d6';
      case 2:
        return '#4161f1';
      case 1:
        return '#66e961';
      case 0:
        return 'black';
      default:
        return 'black';
    }
  }

  /**
   * Devuelve el color del borde del planeta según su tipo
   * @param type Tipo de la carta
   * @returns String con el color del borde
   */
  getPlanetBorderColor(type: IPlanetType): string {
    switch (type) {
      case 2:
        return '#EE0C94';
      case 1:
        return '#81CF00';
      case 0:
        return '#0085CF';
      default:
        return 'black';
    }
  }
}
