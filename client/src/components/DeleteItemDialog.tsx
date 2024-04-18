import { Button } from "./ui/button";
import { Trash2 } from "lucide-react";
import { DialogHeader } from "./ui/dialog";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "./ui/alert-dialog";

interface DeleteItemDialogProps {
  clientId: number;
  onConfirm: () => void;
}

export default function DeleteItemDialog({
  clientId,
  onConfirm,
}: DeleteItemDialogProps) {
  return (
    <AlertDialog>
      <AlertDialogTrigger>
        <Button variant={"ghost"} size={"icon"}>
          <Trash2 />
        </Button>
      </AlertDialogTrigger>
      <AlertDialogContent>
        <DialogHeader>
          <AlertDialogTitle>Deletar cliente?</AlertDialogTitle>
        </DialogHeader>
        <AlertDialogDescription>
          Tem certeza que deseja deletar este cliente com ID: {clientId}?
        </AlertDialogDescription>
        <AlertDialogFooter>
          <AlertDialogAction asChild>
            <Button onClick={onConfirm}>Sim</Button>
          </AlertDialogAction>
          <AlertDialogCancel asChild>
            <Button variant={"ghost"}>Não</Button>
          </AlertDialogCancel>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  );
}
