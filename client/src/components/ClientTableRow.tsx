import type Client from "@/services/models/client";
import DeleteItemDialog from "./DeleteItemDialog";
import { TableCell, TableRow } from "./ui/table";
import { deleteClient } from "@/services/api";
import { toast } from "./ui/use-toast";

interface ClientTableRowProps {
    client: Client;
}

export default function ClientTableRow({ client }: ClientTableRowProps) {
  return (
    <TableRow>
      <TableCell>{client.id}</TableCell>
      <TableCell>{client.name}</TableCell>
      <TableCell>{client.email}</TableCell>
      <TableCell>
        <DeleteItemDialog
          clientId={client.id}
          onConfirm={async () => {
            const success = await deleteClient(client.id);
            if(success) {
                toast({
                    title: "Cliente deletado",
                    description: `Cliente com ID: ${client.id} deletado com sucesso.`,
                });
            }
            else {
                toast({
                    title: "Erro ao deletar cliente",
                    description: `Não foi possível deletar o cliente com ID: ${client.id}.`,
                    variant: "destructive"
                });
            }
          }}
        />
      </TableCell>
    </TableRow>
  );
}