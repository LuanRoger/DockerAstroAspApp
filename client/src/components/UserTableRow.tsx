import type User from "../services/models/user";

interface UserTableRowProps {
  user: User;
}

export default function UserTableRow({ user }: UserTableRowProps) {
  return (
    <tr>
      <td>{user.id}</td>
      <td>{user.name}</td>
      <td>{user.email}</td>
    </tr>
  );
}
