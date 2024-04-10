import { useEffect, useState } from "react";
import { getUsers } from "../services/api";
import { useStore } from "@nanostores/react";
import usersStore from "../stores/users_store";
import pageStore from "../stores/page_store";
import type Client from "../services/models/client";
import {
  Table,
  TableBody,
  TableHead,
  TableHeader,
  TableRow,
} from "./ui/table";
import ClientTableRow from "./ClientTableRow";

export default function UserTable() {
  const $page = useStore(pageStore);
  const $users = useStore(usersStore);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    async function fetchUsers(page: number) {
      const users = await getUsers(page);
      usersStore.set(users);
    }

    setIsLoading(true);
    Promise.all([fetchUsers($page)]).then(() => setIsLoading(false));
  }, [$page]);

  if (isLoading) {
    return <UsersTableLoading />;
  }

  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead>ID</TableHead>
          <TableHead>Nome</TableHead>
          <TableHead>Email</TableHead>
          <TableHead>Ações</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {$users.map((client: Client) => (
          <ClientTableRow client={client} />
        ))}
      </TableBody>
    </Table>
  );
}

function UsersTableLoading() {
  return (
    <div className="flex flex-row justify-center font-bold">
      <span className="animate-spin">⌛</span>
      <h1>Carregando...</h1>
    </div>
  );
}
