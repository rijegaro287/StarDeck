interface IAvatar {
  id: number;
  name: string;
  image: File;
}
interface ICollection {
  idAccount: string;
  collection: string;
}

interface IAccount {
  id: string;
  name: string;
  nickname: string;
  email: string;
  country: string;
  password: string;
}

export {
  IAccount,
  IAvatar,
  ICollection
}
