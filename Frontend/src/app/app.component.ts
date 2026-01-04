import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { BirthFormComponent } from './components/birth-form/birth-form.component';
import { TuViChartComponent } from './components/tu-vi-chart/tu-vi-chart.component';
import { TuViService } from './services/tu-vi.service';
import { ChartRequest, TuViChart } from './models/tu-vi.models';

@Component({
  selector: 'app-root',
  imports: [CommonModule, RouterOutlet, BirthFormComponent, TuViChartComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [TuViService]
})
export class AppComponent {
  title = 'Tử Vi Đẩu Số';
  chart: TuViChart | null = null;
  loading = false;
  error: string | null = null;

  constructor(private tuViService: TuViService) {}

  onChartGenerated(request: ChartRequest) {
    this.loading = true;
    this.error = null;
    
    this.tuViService.generateChart(request).subscribe({
      next: (chart) => {
        this.chart = chart;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Không thể tạo lá số. Vui lòng kiểm tra kết nối API.';
        this.loading = false;
        console.error('Error generating chart:', err);
      }
    });
  }
}
