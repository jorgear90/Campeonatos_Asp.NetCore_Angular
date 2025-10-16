import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-search-input',
  templateUrl: './search-input.component.html',
  styleUrl: './search-input.component.css'
})
export class SearchInputComponent {
  @Input() options: string[] = []; // Lista de nombres disponibles
  @Output() suggestionSelected = new EventEmitter<string>();
  @Output() cleared = new EventEmitter<void>();
  @Output() search = new EventEmitter<string>();

  searchText = '';
  suggestions: string[] = [];

  // Cuando el usuario escribe, mostramos sugerencias
  onInputChange(): void {
    const value = this.searchText.trim().toLowerCase();

    if (value.length === 0) {
      this.suggestions = [];
      this.cleared.emit();
      return;
    }

    this.suggestions = this.options
      .filter(opt => opt.toLowerCase().includes(value))
      .slice(0, 5);
  }

  // Cuando el usuario selecciona una sugerencia
  selectSuggestion(suggestion: string): void {
    this.searchText = suggestion;
    this.suggestions = [];
    this.suggestionSelected.emit(suggestion);
  }

  // Cuando presiona enter o el botón buscar
  onSearch(): void {
    const value = this.searchText.trim();

    if (value.length > 0) {
      this.suggestions = [];
      this.search.emit(value);
    }
  }
}
