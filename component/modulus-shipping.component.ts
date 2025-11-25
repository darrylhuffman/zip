import { CommonModule } from '@angular/common';
import { Component, computed, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { DividerModule } from 'primeng/divider';
import { TagModule } from 'primeng/tag';

import { WeatherRequestDto, WeatherResponseDto } from '../models/weather.dto';

const WEATHER_ENDPOINT = `https://localhost:7184/api/weather`;

type PackingInstructionUi = {
  label: string;
  description: string;
  severity: 'danger' | 'warning' | 'info';
};

@Component({
  selector: 'app-modulus-shipping',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CardModule,
    InputTextModule,
    ButtonModule,
    MessageModule,
    DividerModule,
    TagModule,
  ],
  templateUrl: './modulus-shipping.component.html',
  styleUrl: './modulus-shipping.component.css',
})
export class ModulusShippingComponent {
  private readonly http = inject(HttpClient);
  private readonly fb = inject(FormBuilder);

  protected readonly requestForm = this.fb.nonNullable.group({
    zip: ['', [Validators.required, Validators.pattern(/^\d{5}(?:-\d{4})?$/)]],
  });

  protected readonly isLoading = signal(false);
  protected readonly errorMessage = signal<string | null>(null);
  protected readonly report = signal<WeatherResponseDto | null>(null);

  protected readonly packingInstruction = computed<PackingInstructionUi | null>(() => {
    const high = this.report()?.main?.temp_max;
    if (typeof high !== 'number') {
      return null;
    }

    if (high >= 93) {
      return {
        label: 'EXTREME HEAT PACKOUT',
        description: 'Max insulation, frozen gel packs, and priority handling required.',
        severity: 'danger',
      };
    }

    if (high <= 40) {
      return {
        label: 'FREEZE PACKOUT',
        description: 'Use freeze packs or dry ice to keep product stable in cold lanes.',
        severity: 'info',
      };
    }

    return {
      label: 'STANDARD PACKOUT',
      description: 'Standard insulation is sufficient for moderate conditions.',
      severity: 'warning',
    };
  });

  protected get zipControl() {
    return this.requestForm.controls.zip;
  }

  protected formatTemp(temp?: number): string {
    if (typeof temp !== 'number') {
      return '—';
    }

    return `${Math.round(temp)}°F`;
  }

  protected async submit(): Promise<void> {
    if (this.requestForm.invalid) {
      this.requestForm.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set(null);

    const payload: WeatherRequestDto = {
      zip: this.zipControl.value.trim(),
      countryCode: 'US',
    };

    try {
      const response = await firstValueFrom(
        this.http.post<WeatherResponseDto>(WEATHER_ENDPOINT, payload)
      );
      this.report.set(response);
    } catch (error) {
      this.report.set(null);
      this.errorMessage.set(this.parseError(error));
    } finally {
      this.isLoading.set(false);
    }
  }

  private parseError(error: unknown): string {
    if (error instanceof HttpErrorResponse) {
      if (typeof error.error === 'string' && error.error) {
        return error.error;
      }

      if (error.error?.message) {
        return error.error.message;
      }

      if (error.status) {
        return `Request failed with status ${error.status}`;
      }
    } else if (typeof error === 'object' && error !== null && 'message' in error) {
      return String((error as { message?: unknown }).message ?? 'Unexpected error.');
    }

    if (typeof error === 'string' && error) {
      return error;
    }

    return 'Unable to retrieve weather right now. Please try again.';
  }
}
