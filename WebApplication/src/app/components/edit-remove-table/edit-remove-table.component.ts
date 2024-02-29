import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-edit-remove-table',
  template: `
    <div class="btn-container">
      <button class="edit-btn" (click)="editar()">
        <svg id="i-edit" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32" width="20" height="20" fill="none" stroke="currentcolor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2">
          <path d="M30 7 L25 2 5 22 3 29 10 27 Z M21 6 L26 11 Z M5 22 L10 27 Z" />
        </svg>
      </button>
      <button class="delete-btn" (click)="remover()">
        <svg id="i-close" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32" width="20" height="20" fill="none" stroke="currentcolor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2">
          <path d="M2 30 L30 2 M30 30 L2 2" />
        </svg>
      </button>
    </div>
  `,
  styles: [
  ]
})
export class EditRemoveTableComponent {
  // @ts-ignore
  @Input() editarFn: (objeto: any) => void;
  // @ts-ignore
  @Input() removerFn: (objeto: any) => void;
  @Input() objeto: any;

  editar(): void {
    if (this.editarFn && this.objeto) {
      this.editarFn(this.objeto);
    }
  }

  remover(): void {
    if (this.removerFn && this.objeto) {
      this.removerFn(this.objeto);
    }
  }
}
