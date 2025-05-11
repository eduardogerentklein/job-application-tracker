# Job Application Tracker

A web application to track job applications, manage their statuses, and organize related information.

## Features

- Add, update, and delete job applications.
- Track application statuses (e.g., applied, interview, offer, rejected, accepted).
- View a list of all job applications with pagination.

## Prerequisites

- Node.js (>= 14.x)
- **pnpm or yarn (preferred)** or npm

## Installation

### 1. Clone the repository:

```bash
git clone https://github.com/eduardogerentklein/job-application-tracker.git
cd job-application-tracker/frontend
```

### 2. Install dependencies:

```bash
pnpm install
# or
yarn install
# or
npm install
```

### 3. Set up environment variables:

Create a `.env` file in the root directory and define the following environment variable:

```bash
NEXT_PUBLIC_API_URL=<your-api-url>
```

This environment variable will be used to connect to the API (backend).

Example:

```bash
# set the <port> according to your docker port or IIS port
NEXT_PUBLIC_API_URL=https://localhost:<port>/
```

### 4. Run the development server:

```bash
pnpm run dev
# or
yarn dev
# or
npm run dev
```

Open [http://localhost:3000](http://localhost:3000) with your browser to see the result.

## Usage

- To add a new job application, click on the "Add Application" button and fill in the details.
- To update an existing application, click on the dropdown under "Actions" column in the list, make your changes, and click "Update Application".
- To delete an application, click on the dropdown under "Actions" column in the list and then click the "Delete" button.

## Technologies

This project is built with the following technologies:

- [Next.js](https://nextjs.org) - React framework for building server-side rendered applications.
- [TypeScript](https://www.typescriptlang.org/) - A superset of JavaScript that adds static types.
- [Axios](https://axios-http.com/) - Axios is a promise based HTTP client.
- [shadcn/ui](https://ui.shadcn.com/) - A set of beautifully-designed, accessible components and a code distribution platform.
- [Tailwind CSS v4](https://tailwindcss.com/) - A utility-first CSS framework for styling.
- [Zod](https://zod.dev/) - Schema validation library.
- [lucide-react](https://lucide.dev/) - Open-source icon library.
