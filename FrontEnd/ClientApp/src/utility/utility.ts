import { Router } from "@angular/router";

export default class Utility {

  // Redirect della pagina
  static redirect(url: string, router: Router): void {
    router.navigate([]).then(res => { window.open(url, '_self') });
  }
}
