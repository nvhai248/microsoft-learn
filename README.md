# microsoft-learn
A repo learn about asp.net .net api and entity framework 

# Web security

## Injection attack

### Injection attack type
1. SQL
2. Command
3. CLRF: Carriage Return (ASCII 13, /r) Line Feed (ASCII 10, \n)

### File upload attack

1. What are the risk?

- Unauthorized uploads
- Malicious content
- Overwriting an existing file
- Very large file upload

2. How to Prevent File Upload Attacks?

- Authenticate users
- Verify and allow only specific file extensions
- Set maximum name length and file size
- Store uploaded file outside the webroot
- Use simple error messages

### Authentication attack

Prevent Authentication Attack:
- Multifactor authentication (MFA)
- Lockout: limit authentication attempts
- Password hashing
- Training

### XSS and CSRF

- Cross-Site Scripting (XSS): 
  - Attacker inject client-side code into you web application, for example, in an input or text area.
  - Prevent: 
    -  Encode (HTML, JavaScript, URL Parameters)
    -  Validation and test
- Cross-Site Request Forgery (CSRF):
  - Attack user authenticated user sessions to send unwanted requests to a web application or site from an authenticated user.
  - Prevent: 
    - same-site cookies
    - Enable user interaction
    - Custom request headers

### Cors

Attacker use third-party applications and tools to access your application.
