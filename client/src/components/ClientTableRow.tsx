import type Client from "@/services/models/client";
import DeleteItemDialog from "./DeleteItemDialog";
import { TableCell, TableRow } from "./ui/table";
import { deleteClient } from "@/services/api";
import { toast } from "./ui/use-toast";
import UpdateClientDialog from "./UpdateClientDialog";

interface ClientTableRowProps {
  client: Client;
  onClientDeleted?: (clientId: number) => void | undefined;
  onClientUpdated?: (client: Client) => void | undefined;
}

export default function ClientTableRow({
  client,
  onClientDeleted,
  onClientUpdated,
}: ClientTableRowProps) {
  async function handleClientDeleteConfirm() {
    const success = await deleteClient(client.id);
    if (success) {
      toast({
        title: "Cliente deletado",
        description: `Cliente com ID: ${client.id} deletado com sucesso.`,
      });
      onClientDeleted?.(client.id);
    } else {
      toast({
        title: "Erro ao deletar cliente",
        description: `Não foi possível deletar o cliente com ID: ${client.id}.`,
        variant: "destructive",
      });
    }
  }

  function handleClientUpdate(newClientInfo: Client) {
    toast({
      title: "Cliente atualizado",
      description: `Cliente com ID: ${newClientInfo.id} atualizado com sucesso.`,
    });
    onClientUpdated?.(newClientInfo);
  }

  function handleClientUpdateFail() {
    toast({
      title: "Erro ao atualizar cliente",
      description: `Não foi possível atualizar o cliente com ID: ${client.id}.`,
    });
  }

  return (
    <TableRow>
      <TableCell>{client.id}</TableCell>
      <TableCell>{client.name}</TableCell>
      <TableCell>{client.email}</TableCell>
      <TableCell>
        <DeleteItemDialog
          clientId={client.id}
          onConfirm={handleClientDeleteConfirm}
        />
        <UpdateClientDialog
          clientToUpdate={client}
          onConfirm={handleClientUpdate}
          onFail={handleClientUpdateFail}
        />
      </TableCell>
    </TableRow>
  );
}
