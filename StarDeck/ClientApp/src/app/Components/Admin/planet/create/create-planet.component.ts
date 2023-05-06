import * as random from "random-web-token";
import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IPlanet, IPlanetType } from '../../../../Interfaces/Planet';
import { HelpersService } from '../../../../Services/helpers.service';
import { PlanetService } from '../../../../Services/planet.service';

@Component({
  selector: 'app-card-form',
  templateUrl: './create-planet.component.html',
  styleUrls: ['./create-planet.component.scss']
})
export class CreatePlanetComponent {
  /** Recibe un formulario como entrada */
  newPlanet: FormGroup;

  constructor(
    private _formBuilder: FormBuilder,
    private planetService: PlanetService,
    private helpers: HelpersService
  ) {
    this.newPlanet = this._formBuilder.group({
      name: ['', Validators.required],
      type: ['', Validators.required],
      description: ['', Validators.required]
    });
  }
  /*
   *Funcion que crea la cuenta del nuevo jugador 
   */
  async createPlanet() {
    const fileInput: HTMLInputElement = document.querySelector('#file-input')!;
    const fileList: FileList = fileInput.files!;

    const imageString = fileList.length ? await this.helpers.fileToBase64(fileList[0]) : '';

    
    const newPlanet: IPlanet = {
      id: 'P-' + random.genSync('medium+', 12),
      name: this.newPlanet.value.name.toString(),
      image: imageString,
      active: true,
      type: Number(this.newPlanet.value.type) as IPlanetType,
      description: this.newPlanet.value.description
    };
    console.log(newPlanet)

    try {
      this.validatePlanet(newPlanet);

      await this.planetService.addPlanet(newPlanet)
        .then(response => {
          console.log(response);
          window.location.reload();
        });
    } catch (error) {
      alert(error);
    }


  }

  /**
   * Valida que las entradas del formulario sean correctas
   * @param newPlanet Planeta a validar
   * @throws Error si alguna entrada es incorrecta o no está llena
   */
  validatePlanet(newPlanet: IPlanet): void {
    Object.keys(newPlanet).forEach(key => {
      const value = newPlanet[key as keyof IPlanet];
      if (value === '' || value === null || value === undefined) {
        if (key !== 'image') {
          throw new Error('Todos los campos son obligatorios');
        }
      }
    });

    if (newPlanet.name.length < 5 || newPlanet.name.length > 30) {
      throw new Error('El nombre del planeta debe tener entre 5 y 30 caracteres');
    }
    if (newPlanet.description.length > 1000) {
      throw new Error('La descripción del planet debe tener como máximo 1000 caracteres');
    }
  }
 
  
}
