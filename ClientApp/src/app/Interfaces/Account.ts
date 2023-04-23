interface IAvatar {
  id: number;
  name: string;
  image: File;
}
interface IDeck {
  idAccount: string;
  deck: string;
}

interface IAccount {
  id: string;
  name: string;
  nickname: string;
  email: string;
  nationality: string;
  password: string;
  active: boolean;
  avatar: number;
  config: string;
  points: number;
  coins: number;
  //avatarnavigation: IAvatar;
  deck: IDeck;

}

export {
  IAccount,
  IAvatar,
  IDeck
}
