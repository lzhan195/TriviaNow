# TriviaNow

TriviaNow is a web application that allows users to play trivia games and test their knowledge across various categories. This repository contains the source code and project files for TriviaNow.

## Features

- User registration and authentication
- Multiple trivia categories to choose from
- Randomized questions for each game session
- Score tracking and leaderboard
- Timed gameplay
- Responsive design for different screen sizes

## Installation

To run TriviaNow locally, follow these steps:

1. Clone this repository to your local machine:

```bash
git clone https://github.com/lzhan195/TriviaNow.git
```

2. Navigate to the project directory:

```bash
cd TriviaNow
```

3. Install the required dependencies:

```bash
npm install
```

4. Configure the environment variables:

- Create a `.env` file in the root directory.
- Set the following variables in the `.env` file:
  - `DB_HOST`: The hostname of your database server
  - `DB_PORT`: The port number of your database server
  - `DB_NAME`: The name of the database
  - `DB_USER`: The username for connecting to the database
  - `DB_PASSWORD`: The password for connecting to the database
  - `JWT_SECRET`: A secret key for JWT authentication

5. Start the development server:

```bash
npm start
```

6. Open your web browser and access `http://localhost:3000` to view TriviaNow.

## Technologies Used

- Node.js
- Express.js
- MongoDB
- Mongoose
- Passport.js
- JWT
- HTML/CSS
- Bootstrap

## License

TriviaNow is licensed under the [MIT License](LICENSE).
