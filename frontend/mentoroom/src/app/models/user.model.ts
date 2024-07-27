import { jwtDecode } from 'jwt-decode';
import Tokens from '../interfaces/tokens.interface';

interface UserData {
  Id: string;
  Email: string;
  FirstName: string;
  LastName: string;
  Role: string | null;
}

export class User {
  tokens: Tokens;
  userData: UserData;

  constructor(tokens: Tokens) {
    this.tokens = tokens;
    this.userData = this.decodeUserDataFromToken(this.tokens);
  }

  private decodeUserDataFromToken(tokens: Tokens): UserData {
    const decodedToken: any = jwtDecode(tokens.accessToken);
    const userData: UserData = {
      Id: decodedToken.Id,
      Email: decodedToken.Email,
      FirstName: decodedToken.FirstName,
      LastName: decodedToken.LastName,
      Role:
        decodedToken[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ] || null,
    };
    return userData;
  }

  get getAccessTokens() {
    return this.tokens;
  }

  get getUserRole() {
    return this.userData?.Role ?? null;
  }

  get getFirstName() {
    return this.userData?.FirstName ?? null;
  }
}
