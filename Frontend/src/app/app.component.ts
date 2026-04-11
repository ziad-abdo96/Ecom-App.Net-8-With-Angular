
import { Component, OnInit } from '@angular/core';
import { LoadingService } from './core/Services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'E-commerce';
  constructor(private loadingService: LoadingService) {}

  ngOnInit(): void {
      // this.loadingService.Loading();
      // setTimeout(() => {
      //   this.loadingService.HideLoader();
      // }, 0);
  }
}
