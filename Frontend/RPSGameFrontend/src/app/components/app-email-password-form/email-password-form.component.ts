import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-email-password-form',
  standalone: true, // <-- обязательно
  imports: [CommonModule, ReactiveFormsModule, RouterModule], 
  templateUrl: './email-password-form.component.html',
  styleUrls: ['./email-password-form.component.scss'] // <-- исправлено
})
export class EmailPasswordFormComponent {
  form: FormGroup;
  loading = false;
  errorMessage = '';

  @Output() formSubmitted = new EventEmitter<{email: string, password: string}>();

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.formSubmitted.emit(this.form.value);
  }
}
