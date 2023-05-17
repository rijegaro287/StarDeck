import * as Phaser from "phaser";

import { COLORS } from "../Constants";
import { toRGBString } from "../Helpers";

export default class Card extends Phaser.GameObjects.Container {
	private base64Image?: string;
	private margin: number;
	private cardName: string;
	private cardRace: string;
	private cardEnergyCost: number;
	private cardBattleCost: number;

	constructor(
		scene: Phaser.Scene,
		x: number,
		y: number,
		cardName: string,
		cardRace: string,
		cardEnergyCost: number,
		cardBattleCost: number,
		base64Image?: string
	) {
		super(scene, x, y);

		this.x = x;
		this.y = y;
		this.width = 200;
		this.height = 350;
		this.margin = 15;
		this.cardName = cardName;
		this.cardRace = cardRace;
		this.cardEnergyCost = cardEnergyCost;
		this.cardBattleCost = cardBattleCost;

		this.base64Image = base64Image ? base64Image : '';

		this.setSize(this.width, this.height);
		this.setPosition(this.x, this.y);


		const cardBackground = this.scene.add.rectangle(
			0, 0, this.width, this.height, COLORS.BACKGROUND_1, 1
		);

		const imageSize = this.width - 2 * this.margin;
		const imagePositionY = -(this.height - imageSize) / 2 + this.margin
		const cardImage = this.scene.add.image(0, imagePositionY, 'default-card');
		cardImage.setDisplaySize(imageSize, imageSize);


		console.log(cardImage.displayHeight, cardImage.displayWidth);


		if (this.base64Image !== '') {
			this.scene.textures
				.addBase64(cardName, `data:image/png;base64,${this.base64Image}`)
				.once(Phaser.Textures.Events.LOAD, () => cardImage.setTexture(cardName));
		}

		const cardNameText = this.scene.add.text(0, 2 * this.margin, this.cardName, {
			fontFamily: 'Exo, sans-serif',
			fontSize: '30px',
			fontStyle: 'bold',
			color: toRGBString(COLORS.SECONDARY)
		});
		cardNameText.setOrigin(0.5, 0.5);

		const cardRaceText = this.scene.add.text(0, 4.5 * this.margin, this.cardRace, {
			fontFamily: 'Exo, sans-serif',
			fontSize: '25px',
			fontStyle: 'bold',
			color: toRGBString(COLORS.WHITE)
		});
		cardRaceText.setOrigin(0.5, 0.5);


		const energyIcon = this.scene.add.image(0, 0, 'energy-icon');
		const energyIconPositionX = -(this.width - energyIcon.width) / 2 + 2 * this.margin;
		const energyIconPositionY = 7.5 * this.margin;
		energyIcon.setPosition(energyIconPositionX, energyIconPositionY);

		const energyText = this.scene.add.text(0, 0, `${this.cardEnergyCost}`, {
			fontFamily: 'Exo, sans-serif',
			fontSize: '20px',
			fontStyle: 'bold',
			color: toRGBString(COLORS.WHITE)
		});
		energyText.setPosition(
			energyIconPositionX,
			energyIconPositionY + energyIcon.height / 2 + energyText.height / 2 + 5
		);
		energyText.setOrigin(0.5, 0.5);

		const battleCostIcon = this.scene.add.image(0, 0, 'battle-cost-icon');
		const battleCostIconPositionX = (this.width - battleCostIcon.width) / 2 - 2 * this.margin;
		const battleCostIconPositionY = 7.5 * this.margin;
		battleCostIcon.setPosition(battleCostIconPositionX, battleCostIconPositionY);

		const battleCostText = this.scene.add.text(0, 0, `${this.cardBattleCost}`, {
			fontFamily: 'Exo, sans-serif',
			fontSize: '20px',
			fontStyle: 'bold',
			color: toRGBString(COLORS.WHITE)
		});
		battleCostText.setPosition(
			battleCostIconPositionX,
			battleCostIconPositionY + battleCostIcon.height / 2 + battleCostText.height / 2 + 5
		);
		battleCostText.setOrigin(0.5, 0.5);

		this.add(cardBackground);
		this.add(cardImage);
		this.add(cardNameText);
		this.add(cardRaceText);
		this.add(energyIcon);
		this.add(energyText);
		this.add(battleCostIcon);
		this.add(battleCostText);
	}
}