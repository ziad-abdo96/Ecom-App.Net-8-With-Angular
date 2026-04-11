import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.scss'
})
export class PaginationComponent {
  @Input() pageSize: number;
  @Input() totalCount: number;
  @Output() changePaged = new EventEmitter<number>();
  OnchangePage(event: any) {
    this.changePaged.emit(event.page);
  }
}
