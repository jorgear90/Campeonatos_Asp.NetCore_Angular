import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-return-button',
  templateUrl: './return-button.component.html',
  styleUrl: './return-button.component.css'
})
export class ReturnButtonComponent {
  @Input() returnPath: string = '/main';
  @Input() buttonText: string = 'Volver';

  constructor(private router: Router) { }

  mainButton() {
    this.router.navigate([this.returnPath]);
  }
}
