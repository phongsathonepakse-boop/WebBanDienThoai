# Use IIS with ASP.NET Framework
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8

# Set working directory
WORKDIR /inetpub/wwwroot

# Copy project files
COPY . .

# Expose port
EXPOSE 80
