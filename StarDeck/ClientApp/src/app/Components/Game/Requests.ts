import { apiURL } from "src/app/app.component";

const url = `${apiURL}/api/Game`

const getUserGameRoomData = (userID: string, gameRoomID: string) => {
  return fetch(`${url}/getGameRoomData/${gameRoomID}/${userID}`, {
    method: 'GET',
    credentials: 'same-origin',
    headers: { 'Content-Type': 'application/json' }
  });
}

export {
  getUserGameRoomData
}