export class UpdateClient {
  newName: string;
  newEmail: string;

  constructor(name: string, email: string) {
    this.newName = name;
    this.newEmail = email;
  }
}