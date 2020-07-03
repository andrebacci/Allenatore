export class Team {
  id: number = 0;
  name: string = "";
  city: string = "";
  mister: string = "";
  idCategory: number = 0;
  logo: string = "";

  category: string = "";

  constructor(name: string) {
    this.name = name;
  }
}
