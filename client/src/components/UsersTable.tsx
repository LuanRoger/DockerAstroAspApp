import { useEffect, useState } from "react";
import { getUsers } from "../services/api"
import { useStore } from "@nanostores/react";
import usersStore from "../stores/users_store";
import pageStore from "../stores/page_store";
import type User from "../services/models/user";
import UserTableRow from "./UserTableRow";

export default function UserTable() {
  const $page = useStore(pageStore)
  const $users = useStore(usersStore)
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    async function fetchUsers(page:number) {
      const users = await getUsers(page)
      usersStore.set(users)
    }

    setIsLoading(true)
    Promise.all([fetchUsers($page)]).then(() => setIsLoading(false))
  }, [$page])

  if(isLoading) {
    return <UsersTableLoading />
  }

  return (
    <table className="w-full text-center">
      <tr className="bg-slate-500 text-lg">
        <th>ID</th>
        <th>Nome</th>
        <th>Email</th>
      </tr>
      {$users.map((user: User) => (
        <UserTableRow user={user} />
      ))}
    </table>
  );
}

function UsersTableLoading() {
  return (
    <div className="flex flex-row justify-center font-bold">
      <span className="animate-spin">⌛</span>
      <h1> Carregando...</h1>
    </div>
  )
}