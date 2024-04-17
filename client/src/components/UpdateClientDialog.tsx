import { Edit } from "lucide-react";
import { Button } from "./ui/button";
import { Dialog, DialogContent, DialogTrigger } from "./ui/dialog";
import { DialogTitle } from "@radix-ui/react-dialog";
import UpdateClientForm from "./UpdateClientForm";
import type Client from "@/services/models/client";
import { useState } from "react";
import type { ClientFormValues } from "@/lib/schemas/client_form_schema";
import { updateClient } from "@/services/api";
import { UpdateClient } from "@/services/models/update_client";

interface UpdateClientDialogProps {
  clientToUpdate: Client;
  onConfirm?: (newClientInfo: Client) => void | undefined;
  onFail?: () => void | undefined;
}

export default function UpdateClientDialog({
  clientToUpdate,
  onConfirm,
  onFail
}: UpdateClientDialogProps) {
  const [open, setOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  async function handleSubmit(newClientInfo: ClientFormValues) {
    setIsLoading(true);
    
    const updateClientInfo = new UpdateClient(newClientInfo.name, newClientInfo.email)
    const result = await updateClient(clientToUpdate.id, updateClientInfo);

    if(result === null) {
        setIsLoading(false);
        onFail?.();
        return;
    }

    setIsLoading(false);
    setOpen(false);
    onConfirm?.(result)
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger>
        <Button variant="ghost" size="icon">
          <Edit />
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogTitle>Atualizar Cliente</DialogTitle>
        <UpdateClientForm
          clientToUpdate={clientToUpdate}
          onSubmitClick={handleSubmit}
          isLoadingButton={isLoading}
        />
      </DialogContent>
    </Dialog>
  );
}
