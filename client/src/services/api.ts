import axios from "axios";
import type User from "./models/user";

const baseUrl = import.meta.env.PUBLIC_API_ENDPOINT
const usersEndpoint = `${baseUrl}/user`

export async function getUsers(page: number): Promise<User[]> {
    const response = await axios.get<User[]>(usersEndpoint, {
        params: {
            page: page,
            pageSize: 10
        }
    })
    if (response.status !== 200) {
        return []
    }
    
    return response.data
}