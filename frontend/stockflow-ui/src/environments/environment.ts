// Development environment configuration
// Used when running the Angular app locally with ng serve.

export const environment = {
  production: false,

  // Local ASP.NET Core API base URL.
  // IMPORTANT:
  // - Do not add trailing slash
  // - Keep "/api" because backend controllers use routes like api/[controller]
  apiBaseUrl: 'http://localhost:5118/api'
};
