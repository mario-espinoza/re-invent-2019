# Securing enterprise-grade serverless apps @Gerardo Estraba

## Speed + Security

## Where to start? -> Identity

User credentials

- Stored in plain text
- Hash is not enough
- +/- Salt Hashing Credentials
- OK: Secure remote Pasword protocol SRP

  - Cognito

    - User Pools
    - Identitu Tools

- Managing multiple identities -> No! increases attack surface
  OK: Centralize identity management

  Serverless is JWT aware

## Security at all Layers

- Reduce DDoS atack surface: -> AWS Shield +
- XSS -> AWS WAF
- SQLiç

## Secure coding practices
