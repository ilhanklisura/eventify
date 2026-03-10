import { TokenApi } from './resources/token'
import { UserApi } from './resources/user'
import { EventApi } from './resources/event'
import { CategoryApi } from './resources/category'
import { VenueApi } from './resources/venue'
import { TicketApi } from './resources/ticket'
import { BookingApi } from './resources/booking'
import { CodebookApi } from './resources/codebook'

const headers = new Headers()

export function useApi() {
  const clearToken = () => headers.delete('Authorization')
  const setToken = (tokenValue: string) => headers.set('Authorization', `Bearer ${tokenValue}`)

  return {
    clearToken,
    setToken,
    tokenApi: new TokenApi(headers),
    userApi: new UserApi(headers),
    eventApi: new EventApi(headers),
    categoryApi: new CategoryApi(headers),
    venueApi: new VenueApi(headers),
    ticketApi: new TicketApi(headers),
    bookingApi: new BookingApi(headers),
    codebookApi: new CodebookApi(headers),
  }
}
